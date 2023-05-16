using System;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

namespace Blazor_MultiLanguage
{
	/// <summary>
	/// The class that will add an extension method
	/// to WebAssemblyHost class that
	/// will read the selected culture from LocalStorage
	/// and apply to the UI
	/// For this purpose this will use JS Interoperability
	/// to read the default value set for the culture
	/// </summary>
	public static class CultureExtensions
	{
        public async static Task SetDefaultUICulture(this WebAssemblyHost host)
        {
            // 1. Read the IJSRuntime to read the JavaScript
            var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
            // 2. Read the current value for the Culture from the
            // LocalStorage from the UICulture object
            var result = await jsInterop.InvokeAsync<string>("UICulture.get");
            CultureInfo culture;
            if (result != null)
                // 3.a. Set the selected Culture
                culture = new CultureInfo(result);
            else
                // 3.b. else the default culture will be en-US
                culture = new CultureInfo("en-US");
            // Set the cukture to application
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}

