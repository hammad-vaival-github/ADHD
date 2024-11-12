using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using waats.Classes;
using waats.Models.data;
using waats.ViewModel;

namespace waats.Controllers
{
    public class GraphController : Controller
    {
        private ManageQueries _Managequeries = new ManageQueries();
        public ActionResult CGraph()
        {
            double ucl = Math.Round(5.4) * 100;
            double lcl = Math.Round(3.2) * 100;
            double cl = Math.Round(2.4) * 100;
            Highcharts chart = new Highcharts("dswq")//Regex.Replace("Daily commitment Graph", @"\s+", ""))
            .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, MarginTop = 1, BorderColor = System.Drawing.Color.Gray, BorderWidth = 2, BackgroundColor = new BackColorOrGradient(System.Drawing.Color.Transparent) })

                .SetTitle(new Title { Text = "Daily commitment Graph" })
                .SetSubtitle(new Subtitle { Text = string.Empty })
                .SetXAxis(new XAxis
                {
                    //Categories = tempStatus,
                    ///Categories = ViewBag.Daydates.ToArray(),
                    Title = new XAxisTitle
                    {
                        Text = "Dates ==>",
                        Align = AxisTitleAligns.Low
                    },
                    Labels = new XAxisLabels
                    { Rotation = 90 }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0 - 10,
                    Max = 150,
                    LineWidth = 0,
                    GridLineWidth = 0,
                    GridLineColor = System.Drawing.Color.Transparent,
                    //MinorGridLineWidth = 0,
                    //LineColor = System.Drawing.Color.Transparent,
                    //MinorTickLength = 0,
                    //TickPositioner=new[] { new JsonFormatter { JsonValueFormat=new jso } },
                    TickInterval = 5,
                    TickLength = 0,
                    Title = new YAxisTitle
                    {
                        //   Text = SubmitButton == "ComplianceNumber" ? "Compliance Number ==>": "Compliance % ==>",
                        Text = "Compliance % ==>",
                        Align = AxisTitleAligns.High
                    },
                    PlotLines = new[]
                                      {
                                          new YAxisPlotLines
                                          {
                                              Value = Math.Round(lcl, 0, MidpointRounding.AwayFromZero),
                                              Width = 2,
                                              Color = System.Drawing.Color.Red,
                                              Label = new YAxisPlotLinesLabel
                                                {
                                                    Text = "LCL -> "+Math.Round(lcl, 0, MidpointRounding.AwayFromZero) ,
                                                    Align = HorizontalAligns.Left,
                                                    Style = "color:'Red'",
                                                },
                                              DashStyle=DashStyles.Solid
                                          },
                        new YAxisPlotLines
                                          {
                                              Value =Math.Round(ucl, 0, MidpointRounding.AwayFromZero) ,
                                              Width = 2,
                                              Color = System.Drawing.Color.DarkOrange,
                            Label = new YAxisPlotLinesLabel
                            {
                                                    Text = "UCL -> "+Math.Round(ucl, 0, MidpointRounding.AwayFromZero) ,
                                                    Align = HorizontalAligns.Left,
                                                   Style = "color:'Orange'",
                                                },
                                              DashStyle=DashStyles.Solid
                                          },
                                          new YAxisPlotLines
                                          {
                                              Value = Math.Round(cl, 0, MidpointRounding.AwayFromZero),
                                              Width = 2,
                                              Color = System.Drawing.Color.Green,
                                              Label = new YAxisPlotLinesLabel
                                                {
                                                    Text = "CL -> "+Math.Round(cl, 0, MidpointRounding.AwayFromZero) ,
                                                    Align = HorizontalAligns.Left,
                                                    Style = "color:'Green'",
                                                },
                                              DashStyle=DashStyles.Solid
                                          }

                                      }
                })
                //.SetTooltip(new Tooltip
                //{
                //    Formatter = SubmitButton == "ComplianceNumber" ? "function() { return '<b>'+this.series.name +'</b><br/><b>Year/Month : </b>'+this.x +'<br/><b>Compliance Rate : </b>'+ this.y+' %'; }" : "function() { return '<b>'+this.series.name +'</b><br/><b>Year/Month : </b>'+this.x +'<br/><b>Compliance % : </b>'+ this.y+'%'; }"
                //})
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        DataLabels = new PlotOptionsColumnDataLabels { Enabled = true }
                    }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Horizontal,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = 0,
                    Y = 20,
                    Floating = true,
                    BorderWidth = 1,
                    Enabled = true,
                    BackgroundColor = new BackColorOrGradient(System.Drawing.ColorTranslator.FromHtml("#b2b2b2")),
                    Shadow = true
                })
                .SetCredits(new Credits { Enabled = false })
            //.SetSeries(new[]
            //{

                                     /// new Series { Name = chartName+" Compliance ",Color=System.Drawing.Color.Blue, Data = new Data(MySeriesData.Select(y=>new Point{Y=y}).ToArray()) },
                                           
                                            /// DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                                                .SetXAxis(new XAxis
                                                            {
                                                                Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                                                            })
                                                .SetSeries(new Series
                                                            {
                                                                Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
                                                            });
            return View(chart);


        ///});
            return View();
        }

    }
}