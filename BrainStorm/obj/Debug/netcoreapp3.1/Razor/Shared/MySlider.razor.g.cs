#pragma checksum "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\Shared\MySlider.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c2f1162a5fd73ee008aab5f11c82b18eac80dce2"
// <auto-generated/>
#pragma warning disable 1591
namespace BrainStorm.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using BrainStorm;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using BrainStorm.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using ChartJs.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using ChartJs.Blazor.Common;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using ChartJs.Blazor.Common.Axes;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using ChartJs.Blazor.Common.Axes.Ticks;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using ChartJs.Blazor.Common.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using ChartJs.Blazor.Common.Handlers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using ChartJs.Blazor.Common.Time;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using ChartJs.Blazor.Util;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using ChartJs.Blazor.Interop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using ChartJs.Blazor.PieChart;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using MatBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\_Imports.razor"
using BlazorAnimate;

#line default
#line hidden
#nullable disable
    public partial class MySlider : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<MatBlazor.MatSlider<int>>(0);
            __builder.AddAttribute(1, "Style", "color: #4C3823 !important");
            __builder.AddAttribute(2, "Discrete", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 2 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\Shared\MySlider.razor"
                                                                               true

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(3, "Pin", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 2 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\Shared\MySlider.razor"
                                                                                          true

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "ValueMin", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<int>(
#nullable restore
#line 2 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\Shared\MySlider.razor"
                                                                                                                        Min

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(5, "ValueMax", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<int>(
#nullable restore
#line 2 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\Shared\MySlider.razor"
                                                                                                                                        Max

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<int>(
#nullable restore
#line 2 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\Shared\MySlider.razor"
                                                             MyBind

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(7, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<int>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<int>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => MyBind = __value, MyBind))));
            __builder.AddAttribute(8, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<int>>>(() => MyBind));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 4 "D:\Work\BrainStorm charts\BrainStorm\BrainStorm\BrainStorm\Shared\MySlider.razor"
       
    [Parameter]
    public int MyBind { get; set; }

    [Parameter]
    public int Min { get; set; }

    [Parameter]
    public int Max { get; set; }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591