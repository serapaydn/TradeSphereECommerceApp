using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TradeBank.Models;

namespace TradeBank.Controllers
{
    public class PayController : ApiController
    {
        TradeBank_DBEntities db = new TradeBank_DBEntities();

        [Route("api/pay")]
        public string Post(string kartNo, string ay, string yil, string cvv, double bakiye, string merchantID, string merchantPass)
        {
            int saticisayi = db.SanalPosMusterileri.Count(sm => sm.SaticiKodu == merchantID && sm.SaticiSifre == merchantPass);
            if (saticisayi > 0)
            {
                SanalPosMusterileri spm = db.SanalPosMusterileri.First();
                if (Convert.ToBoolean(spm.Durum))
                {
                    int sayi = db.Kartlar.Count(k => k.KartNo == kartNo);
                    if (sayi > 0)
                    {

                        Kartlar kart = db.Kartlar.First(k => k.KartNo == kartNo);
                        Hesap hesap = db.Hesap.Find(kart.HesapID);
                        if (cvv == kart.CVV)
                        {
                            bool chekdate = false;
                            if (Convert.ToInt32(yil) > DateTime.Now.Year)
                            {
                                chekdate = true;
                            }
                            else
                            {
                                if (Convert.ToInt32(yil) >= DateTime.Now.Year && Convert.ToInt32(ay) >= DateTime.Now.Month)
                                {
                                    chekdate = true;
                                }
                            }
                            if (chekdate)
                            {
                                if (Convert.ToBoolean(kart.KartDurum))
                                {
                                    if (hesap.Bakiye >= Convert.ToDecimal(bakiye))
                                    {
                                        hesap.Bakiye -= Convert.ToDecimal(bakiye);
                                        db.SaveChanges();
                                        return "201";
                                    }
                                    else
                                    {
                                        return "301";
                                    }
                                }
                                else
                                {
                                    return "401";
                                }
                            }
                            else
                            {
                                return "501";
                            }
                        }
                        else
                        {
                            return "801";
                        }
                    }
                    else
                    {
                        return "901";
                    }
                }
                else
                {
                    return "601";
                }
            }
            else
            {
                return "701";
            }

        }
    }
}
