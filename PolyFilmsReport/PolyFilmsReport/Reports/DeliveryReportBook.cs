using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Reporting;

namespace PolyFilmsReport
{
    public class DeliveryReportBook : ReportBook
    {
        public DeliveryReportBook()
        {
            var deliverySlipReportSource = new InstanceReportSource { ReportDocument = new DeliverySlip() };
            this.ReportSources.Add(deliverySlipReportSource);

            var deliveryListReportSource = new InstanceReportSource { ReportDocument = new DeliveryList() };
            this.ReportSources.Add(deliveryListReportSource);

        }
    }
}