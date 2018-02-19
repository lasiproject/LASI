using System;
namespace LASI.WebService.Models
{
    public interface IHierarchicaViewModel<TSelf, TParent> : ILinkedViewModel<TSelf> where TParent : IViewModel
    {
        TParent Parent { get; }
    }
}