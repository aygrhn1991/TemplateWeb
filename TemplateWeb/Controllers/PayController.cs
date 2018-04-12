using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateWeb.Component;
using TemplateWeb.Models.DB;
using WxPayAPI;

namespace TemplateWeb.Controllers
{
    [MemberAuthorize]
    public class PayController : Controller
    {
        EntityDB entity = new EntityDB();
        private pay_order CreateOrder(int productId, string payMethod)
        {
            int id = MemberManager.GetMember().id;
            module_product product = entity.module_product.FirstOrDefault(p => p.id == productId);
            pay_order order = new pay_order()
            {
                member_id = id,
                product_id = productId,
                pay_time = null,
                price = product.price,
                delete = false,
                number = id.ToString().PadRight(6, '0') + DateTime.Now.ToString("yyyyMMddHHmmss"),
                pay_method = payMethod,
                remark = null,
                state_pay = false,
                sys_datetime = DateTime.Now,
            };
            entity.pay_order.Add(order);
            return order;
        }
        public ActionResult PrePay(int productId)
        {
            ViewBag.productId = productId;
            return View();
        }
        public ActionResult WxPay(int productId, string payMethod)
        {
            pay_order order = this.CreateOrder(productId, payMethod);
            module_product product = entity.module_product.FirstOrDefault(p => p.id == order.product_id);
            WxPayData data = new WxPayData();
            data.SetValue("body", product.name);//商品描述
            data.SetValue("attach", "test");//附加数据
            data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());//随机字符串
            data.SetValue("total_fee", (int)Math.Ceiling(order.price.Value * 100));//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            data.SetValue("goods_tag", "test");//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            data.SetValue("product_id", order.product_id);//商品ID
            WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口
            string url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
            ViewBag.url = url;
            return View();
        }
        public ActionResult Alipay()
        {
            return View();
        }
    }
}