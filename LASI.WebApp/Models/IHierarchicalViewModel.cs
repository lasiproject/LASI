using System;
namespace LASI.WebApp.Models
{
    public interface IHierarchicaViewModel<TSelf, TParent> : ILinkedViewModel<TSelf> where TParent : IViewModel
    {
        TParent Parent { get; }
    }
}