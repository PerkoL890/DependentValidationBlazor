# DependentValidation

DependentValidation is a netstandard2.0 library that extends the System.ComponentModel.DataAnnotations with conditional attributes based on another property of the validated object.

The new attributes are the following:

- RequiredIf
- RequiredIfEmpty
- RequiredIfFalse
- RequiredIfNot
- RequiredIfNotEmpty
- RequiredIfNotRegExMatch
- RequiredIfRegExMatch
- RequiredIfTrue
- RegularExpressionIf
- Is
- EqualTo
- GreaterThan
- GreaterThanOrEqualTo
- LessThan
- LessThanOrEqualTo
- NotEqualTo

--- 

### Installation

Note: Currently, the package is not available on NuGet.

### Changes for Blazor WebAssembly (WASM) Compatibility

This package was originally designed with MVC in mind and included client-side validation features. However, these MVC-specific features were not needed for Blazor WebAssembly applications. The original MVC dependencies caused conflicts with Blazor, specifically due to references to Microsoft.AspNetCore.Mvc.ApplicationParts. These caused the following error:
The type or namespace name 'ApplicationPartAttribute' does not exist in the namespace 'Microsoft.AspNetCore.Mvc.ApplicationParts'
The workaround
While it was possible to bypass the error by adding the following to the .csproj file:
<PropertyGroup>
    <GenerateMvcApplicationPartsAssemblyAttributes>false</GenerateMvcApplicationPartsAssemblyAttributes>
</PropertyGroup>
I did not prefer this workaround, so I removed all references to MVC from the package entirely. This modification ensures that the package is suitable for Blazor WASM and works out of the box without the need for MVC or client-side validation.

### Usage Example for Blazor
You can use DependentValidation in your Blazor WebAssembly application to add validation attributes based on other properties in your model. Here's an example of how you can apply the RequiredIfEmpty attribute:
public class MyEntity
{
    [DependentValidation.RequiredIfEmpty("Description")]
    public string Name { get; set; }

    public string Description { get; set; }
}
This example adds the RequiredIfEmpty attribute to the Name property, making it required if the Description property is empty.

Once you’ve added this package (either as a NuGet package or as a local class library) and referenced it in your Blazor project, it should work out of the box.
Example Form in Blazor:

<SfDataForm @ref="form" Model="myEntity" OnUpdate="OnFormUpdate">
    <SfDataForm>
</SfDataForm>
(Should have the same functionality with <EditForm>)
This example uses Syncfusion's DataForm component to create a form that will automatically validate the Name field if the Description field is empty.
### What's Changed in This Fork?

This repository is a remake of the original DependentValidation package. The changes made are specifically aimed at Blazor WebAssembly (WASM) applications. The key changes include:

Removal of MVC-related dependencies: The original package relied on MVC features, which caused issues when used in Blazor WASM. These dependencies have been removed to ensure compatibility with Blazor.

No client-side validation: The client-side validation using jQuery and MVC has been removed, making this package server-side validation only.

No need for workarounds: The workaround <GenerateMvcApplicationPartsAssemblyAttributes>false</GenerateMvcApplicationPartsAssemblyAttributes> is no longer necessary.

This makes the package more lightweight and suitable for Blazor applications where you don’t need client-side validation, and you only need validation based on other properties within the model.

License
This project is open-source under the MIT License. See the LICENSE file for more details.

Original Project Information
This project is based on the Foolproof library, which was originally created for ASP.NET MVC projects. It provides a simplified set of validation attributes for those who require conditional validation rules without the complexity of full-fledged validation libraries.

The original author created this library to fill a gap in validation for .NET Core applications, and I’ve forked it to support Blazor.
Comments from the original creator (Javascript has been removed):
```
Minimum Requirements: **.NET Standard 2.0**.

--- 

Inside this project there is a small js file, [dependentvalidation.unobstrusive.js](https://github.com/mind-ra/DependentValidation/blob/master/dependentvalidation.unobstrusive.js) that extend the [jQuery Validation Plugin](https://jqueryvalidation.org/) and the [jQuery Unobtrusive Validation](https://github.com/aspnet/jquery-validation-unobtrusive) for unobstrusive client validation.

---

This project is based on [foolproof](https://github.com/leniel/foolproof), which I used extensively in my Asp.Net MVC projects.

Going forward developing projects with .Net Core, I felt the need of a Foolproof like library, but what I found is too much complex for my needs.

So I decided to try and give back something to the Open Source Community.
