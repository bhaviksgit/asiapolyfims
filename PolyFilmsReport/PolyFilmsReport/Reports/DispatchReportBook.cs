namespace PolyFilmsReport
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for DispatchReportBook.
    /// </summary>
    public partial class DispatchReportBook : Telerik.Reporting.ReportBook
    {

            public DispatchReportBook()
            {
                this.Reports.Add(new DeliverySlip());
                this.Reports.Add(new DeliveryList());
            }

        
    }
}