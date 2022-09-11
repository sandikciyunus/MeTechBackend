using System;
using System.Text;
using System.Text.Json;
using MediatR;
using MeTech.Domain.Entities;
using MeTech.Model.Backet;
using MeTech.ResponseRequest.Campaign;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MeTech.Business.Handlers
{
	public class CampaignApplyCommandHandler:IRequestHandler<CampaignApplyRequest,CampaignApplyResponse>
	{
        private readonly MeTechContext context;
		public CampaignApplyCommandHandler(MeTechContext context)
		{
            this.context = context;
		}

        public async Task<CampaignApplyResponse> Handle(CampaignApplyRequest request, CancellationToken cancellationToken)
        {
            var response = new CampaignApplyResponse();
            try
            {
                decimal discountPrice=0;
                decimal undiscountPrice=0;
                var backet = context.Backets.Where(p => p.IsDeleted == false && p.Id == request.Campaign.BacketId)
                    .Select(x => new BacketGetModel
                    {
                        Id = x.Id,
                        CampaignId=x.CampaignId,
                        Products = x.Products
                    }).FirstOrDefault();
                if (backet == null)
                {
                    response.ErrorMessage = "Sepet bulunamadı";
                    response.IsSuccess = false;
                    return response;
                }
                if (backet.CampaignId != 0)
                {
                    response.Message = "Sepete daha önce kampanya uygulanmıştır. Aynı anda iki kampanya uygulanamaz.";
                    response.IsSuccess = false;
                    return response;
                }
                var campaign = context.Campaigns.Where(p => p.IsDeleted == false && p.Code == request.Campaign.CampaignCode).FirstOrDefault();
                if (campaign == null)
                {
                    response.ErrorMessage = "Kampanya bulunamadı";
                    response.IsSuccess = false;
                    return response;
                }
                var productList = JsonSerializer.Deserialize<List<BacketProductAddModel>>(backet.Products);
                var campaignProducts = context.CampaignProducts.Where(p => p.CampaignId == campaign.Id).ToList();
                var campaignProducts2 = context.CampaignProducts.Where(p => p.CampaignId == campaign.Id && p.Rate==0).ToList();
                var updateBacket = context.Backets.Where(p => p.Id == backet.Id).FirstOrDefault();
                List<BacketProductAddModel> productList2 = new List<BacketProductAddModel>();
                for (int i = 0; i < productList.Count; i++)
                {
                    backet.GeneralPrice += productList[i].TotalPrice;
                    productList[i].Price = productList[i].Price - ((productList[i].Price * campaign.Rate) / 100);
                    productList[i].TotalPrice = productList[i].TotalPrice - ((productList[i].TotalPrice * campaign.Rate) / 100);
                }
                for (int i = 0; i < campaignProducts.Count; i++)
                {
                    var isProduct = productList.Find(p => p.Id == campaignProducts[i].ProductId);
                    if (campaign.AllProductsRequired)
                    {
                     if (isProduct == null)
                    {
                        response.Message = "Sipariş kampanyaya uygun değil.";
                        response.IsSuccess = false;
                        return response;
                    }
                    else
                    {
                        productList2.AddRange(productList.Where(p => p.Id == campaignProducts[i].ProductId).ToList());

                    }
                    }
                    else
                    {
                        productList2.AddRange(productList.Where(p => p.Id == campaignProducts[i].ProductId).ToList());

                    }

                }
                if (campaignProducts.Count > 0 &&  campaign.IsProductsRequired && !campaign.IsGeneralDiscountPrice)
                {
                    backet.ProductList = productList;
                    //productList.Clear();
                    if (productList2.Count > 0)
                    {
  for (int i = 0; i < productList2.Count; i++)
                    {

                        // var product = productList.Where(p => p.Id == productList[i].Id).FirstOrDefault();

                        if (campaign.Price <= backet.GeneralPrice || campaign.Price == 0)
                        {

                            var campaignProduct = campaignProducts.Where(p => p.ProductId == productList2[i].Id).FirstOrDefault();
                            if (campaignProduct != null)
                            {
                                if (campaignProduct.ProductCount <= productList2[i].Count)
                                {
                                    if (campaignProduct.Rate != 0)
                                    {
                                        var newPrice = productList2[i].Price - ((productList2[i].Price * campaignProduct.Rate) / 100);
                                        discountPrice= newPrice * campaignProduct.ProductCount;
                                        var unDiscountPrice2 = productList2[i].Price * (productList2[i].Count - campaignProduct.ProductCount);
                                        productList2[i].TotalPrice = discountPrice+unDiscountPrice2;
                                    }
                                    else
                                    {
                                    var newCount = productList2[i].Count;
                                          undiscountPrice = productList2[i].Price * newCount;
                                        productList2[i].TotalPrice = undiscountPrice;
                                    }
                                
                                    backet.GeneralPrice += productList2[i].TotalPrice;
                               // backet.GeneralPrice= productList2[i].Price+ (backet.GeneralPrice - ((backet.GeneralPrice * campaignProduct.Rate) / 100));
                                response.Message = "İndirim başarıyla uygulandı";
                                response.IsSuccess = true;
                                }
                                else
                                {
                                    response.Message = "Sipariş kampanyaya uygun değil.";
                                    response.IsSuccess = false;
                                    return response;
                                }
                            }
                            else
                            {
                                response.Message ="Sipariş kampanyaya uygun değil.";
                                response.IsSuccess = false;
                                return response;
                            }


                        }
                        else
                        {
                            response.Message = "Siparişinizin en az " + campaign.Price + " TL tutarında olmalıdır";
                            response.IsSuccess = false;
                            return response;
                        }


                    }
                    }
                    else
                    {
                        response.Message = "Sipariş kampanyaya uygun değil.";
                        response.IsSuccess = false;
                        return response;
                    }
                    updateBacket.CampaignId = campaign.Id;
                    updateBacket.Products = JsonConvert.SerializeObject(productList);
                    context.Backets.Update(updateBacket);
                    context.SaveChanges();

                }
                else if (campaignProducts.Count > 0 && campaign.IsProductsRequired && campaign.IsGeneralDiscountPrice)
                {
                    backet.ProductList = productList;
                    //productList.Clear();
                    if (productList.Count > 0)
                    {
                    for (int i = 0; i < productList.Count; i++)
                    {

                        // var product = productList.Where(p => p.Id == productList[i].Id).FirstOrDefault();

                        if (campaign.Price <= backet.GeneralPrice || campaign.Price == 0)
                        {

                            var campaignProduct = campaignProducts.Where(p => p.CampaignId == campaign.Id).FirstOrDefault();
                            if (campaignProduct != null)
                            {
                                if (campaignProduct.ProductCount <= productList[i].Count)
                                {
                                    productList[i].Price = productList[i].Price - ((productList[i].Price * campaignProduct.Rate) / 100);
                                    productList[i].TotalPrice = productList[i].Price * (productList[i].Count);
                                    backet.GeneralPrice += productList[i].TotalPrice;
                                    response.Message = "İndirim başarıyla uygulandı";
                                    response.IsSuccess = true;
                                }
                                else
                                {
                                    response.Message = "Sipariş kampanyaya uygun değil.";
                                    response.IsSuccess = false;
                                    return response;
                                }
                            }
                            else
                            {
                                response.Message = "Sipariş kampanyaya uygun değil.";
                                response.IsSuccess = false;
                                return response;
                            }


                        }
                        else
                        {
                            response.Message = "Siparişinizin en az " + campaign.Price + " TL tutarında olmalıdır";
                            response.IsSuccess = false;
                            return response;
                        }


                    }
                    }
                    else
                    {
                        response.Message = "Sipariş kampanyaya uygun değil.";
                        response.IsSuccess = false;
                        return response;
                    }
                    updateBacket.CampaignId = campaign.Id;
                    updateBacket.Products = JsonConvert.SerializeObject(productList);
                    context.Backets.Update(updateBacket);
                    context.SaveChanges();

                }
                else
                {
                   /* for (int i = 0; i < productList.Count; i++)
                    {
                        //backet.GeneralPrice += productList[i].TotalPrice;
                        productList[i].Price = productList[i].Price - ((productList[i].Price * campaign.Rate) / 100);
                        productList[i].TotalPrice = productList[i].TotalPrice - ((productList[i].TotalPrice * campaign.Rate) / 100);

                    }*/
                    if (campaign.Price <= backet.GeneralPrice)
                    {
                        var jsonProductList = JsonConvert.SerializeObject(productList);
                        updateBacket.CampaignId = campaign.Id;
                        updateBacket.Products = jsonProductList;
                        context.Backets.Update(updateBacket);
                        context.SaveChanges();
                        response.Message = "İndirim başarıyla uygulandı";
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.Message = "Siparişinizin en az " + campaign.Price + " TL tutarında olmalıdır";
                        response.IsSuccess = false;
                        return response;
                    }


                }

            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }
    }
}

