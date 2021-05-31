using MeetingRoomBookingAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_Unit_Test
{
    [TestClass]
    public class Program_UnitTest
    {
        [TestMethod]
        public void CreateWebHostBuilder_UnitTest()
        {
            var prg = new Program();

            string[] args = new string[] { };
            var iWeb = Program.CreateHostBuilder(args);

            Assert.IsNotNull(iWeb);

        }

    }
}
