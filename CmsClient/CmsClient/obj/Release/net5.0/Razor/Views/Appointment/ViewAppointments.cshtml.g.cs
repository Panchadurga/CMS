#pragma checksum "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "14ee6e826c63d884712131d9e225678d3a3bda2f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Appointment_ViewAppointments), @"mvc.1.0.view", @"/Views/Appointment/ViewAppointments.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\_ViewImports.cshtml"
using CmsClient;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\_ViewImports.cshtml"
using CmsClient.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"14ee6e826c63d884712131d9e225678d3a3bda2f", @"/Views/Appointment/ViewAppointments.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f7902c7627e4ad52732bd52f421b2141ad6fb677", @"/Views/_ViewImports.cshtml")]
    public class Views_Appointment_ViewAppointments : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CmsClient.Models.Schedule>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Choosedoctor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ChooseDoctor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
  
    ViewData["Title"] = "ViewAppointments";

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
Write(Html.Partial("_Navbarmenu"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<p>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14ee6e826c63d884712131d9e225678d3a3bda2f4559", async() => {
                WriteLiteral("<h2>Add Appointment</h2>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</p>\n<h4>ViewAppointments</h4>\n<table class=\"table\">\n    <thead>\n        <tr>\n            <th>\n                ");
#nullable restore
#line 15 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
           Write(Html.DisplayNameFor(model => model.AppointmentId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 18 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
           Write(Html.DisplayNameFor(model => model.PatientId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 21 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
           Write(Html.DisplayNameFor(model => model.Specialization));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 24 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
           Write(Html.DisplayNameFor(model => model.DoctorName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 27 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
           Write(Html.DisplayNameFor(model => model.VisitDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 30 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
           Write(Html.DisplayNameFor(model => model.AppointmentTime));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th></th>\n        </tr>\n    </thead>\n    <tbody>\n");
#nullable restore
#line 36 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>\n                    ");
#nullable restore
#line 40 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
               Write(Html.DisplayFor(modelItem => item.AppointmentId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 43 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
               Write(Html.DisplayFor(modelItem => item.PatientId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 46 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
               Write(Html.DisplayFor(modelItem => item.Specialization));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 49 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
               Write(Html.DisplayFor(modelItem => item.DoctorName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 52 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
               Write(Html.DisplayFor(modelItem => item.VisitDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 55 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
               Write(Html.DisplayFor(modelItem => item.AppointmentTime));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 58 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
               Write(Html.ActionLink("Cancel Appointment", "Delete", new { id = item.AppointmentId }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n            </tr>\n");
#nullable restore
#line 61 "C:\Users\DURGA-1\source\repos\Clinic-Management-System-Using-WebAPI-with-MVC-Client-master\CmsClient\CmsClient\Views\Appointment\ViewAppointments.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CmsClient.Models.Schedule>> Html { get; private set; }
    }
}
#pragma warning restore 1591
