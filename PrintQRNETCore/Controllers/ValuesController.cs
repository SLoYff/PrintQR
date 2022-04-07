using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PrintQRNETCore.Controllers
{
    [Route("api/print")]
    public class ValuesController : Controller
    {
        public class TSCLIB_DLL
        {
            [DllImport("TSCLIB.dll", EntryPoint = "about")]
            public static extern int about();

            [DllImport("TSCLIB.dll", EntryPoint = "openport")]
            public static extern int openport(string printername);

            [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
            public static extern int barcode(string x, string y, string type,
                        string height, string readable, string rotation,
                        string narrow, string wide, string code);
            [DllImport("TSCLIB.dll", EntryPoint = "QRcode")]
            public static extern int qrcode(string x, string y, string ECCLevel,
                        string cellWidth, string mode, string rotation,
                        string code);


            [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
            public static extern int clearbuffer();

            [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
            public static extern int closeport();

            [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
            public static extern int downloadpcx(string filename, string image_name);

            [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
            public static extern int formfeed();

            [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
            public static extern int nobackfeed();

            [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
            public static extern int printerfont(string x, string y, string fonttype,
                            string rotation, string xmul, string ymul,
                            string text);

            [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
            public static extern int printlabel(string set, string copy);

            [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
            public static extern int sendcommand(string printercommand);

            [DllImport("TSCLIB.dll", EntryPoint = "setup")]
            public static extern int setup(string width, string height,
                      string speed, string density,
                      string sensor, string vertical,
                      string offset);

            [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
            public static extern int windowsfont(int x, int y, int fontheight,
                            int rotation, int fontstyle, int fontunderline,
                            string szFaceName, string content);

        }

        public class ID
        {
            public string objectID { get; set; }
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "GET1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "GET "+id+" 成功";
        }

        // POST api/values
        [HttpPost]
        public dynamic Post(ID iD)
        {
            TSCLIB_DLL.openport("USB");
            TSCLIB_DLL.setup("40", "30", "4", "15", "0", "2", "2");                           //Setup the media size and sensor type info
            TSCLIB_DLL.clearbuffer();                                                           //Clear image buffer
            //TSCLIB_DLL.barcode("50", "50", "128", "100", "2", "0", "2", "2", "" + iD.objectID + ""); //Drawing barcode
            //TSCLIB_DLL.qrcode("30", "30", "H", "4", "A", "0", "" + iD.objectID + "");
            //TSCLIB_DLL.printerfont("50", "250", "3", "0", "1", "1", "Print Font Test");        //Drawing printer font
            //TSCLIB_DLL.windowsfont(100, 300, 24, 0, 0, 0, "ARIAL", "Windows Arial Font Test");  //Draw windows font
            //TSCLIB_DLL.downloadpcx("C:\\ASP.NET_in_VCsharp_2008\\ASP.NET_in_VCsharp_2008\\UL.PCX", "UL.PCX");                                         //Download PCX file into printer
            //TSCLIB_DLL.downloadpcx("UL.PCX", "UL.PCX");                                         //Download PCX file into printer
            TSCLIB_DLL.sendcommand("QRCODE 50,50,H,6,A,0,M2, \"" + iD.objectID + "\"");                                //Drawing PCX graphic
            TSCLIB_DLL.printerfont("50", "210", "3", "0", "1", "1", "" + iD.objectID + "");
            TSCLIB_DLL.printlabel("1", "1");                                                    //Print labels
            TSCLIB_DLL.closeport();

            return "已打印";

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
