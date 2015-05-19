
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    category: "Language",
    checkId: "CSE0002:Use getter-only auto properties",
    Justification = "Property is set via DI by the IOC container, necessitating a setter",
    Scope = "member",
    Target = "~P:LASI.WebApp.ViewComponents.DynamicTextEditorViewComponent.Tagger")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    category:"Language",
    checkId: "CSE0002:Use getter-only auto properties",
    Justification = "Property is set via DI by the IOC container, necessitating a setter",
    Scope = "member", 
    Target = "~P:LASI.WebApp.ViewComponents.DynamicTextEditorViewComponent.DocumentStore")]

