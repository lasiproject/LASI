using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for Entity constructs, including <see cref="Noun"/>s, <see cref="NounPhrase"/>s, and Gerunds. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IEntity interface provides for generalization and abstraction over many otherwise disparate element types and Type hierarchies. </para>
    /// </summary>
    public interface IEntity : IVerbalSubject, IVerbalObject, IReferenceable, IPossesser, IPossessable, ILexical
    {
        /// <summary>
        /// Binds an IDescriptor, generally an <see cref="Adjective"/> or <see cref="AdjectivePhrase"/>, as a descriptor of the <see cref="IEntity"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="IDescriptor"/> instance which will be added to the Noun's descriptors.</param>
        void BindDescriptor(IDescriptor descriptor);
        /// <summary>
        /// Gets all of the <see cref="IDescriptor"/> constructs, generally <see cref="Adjective"/>s or <see cref="AdjectivePhrase"/>s, which describe the IDescribable.
        /// </summary>
        IEnumerable<IDescriptor> Descriptors { get; }
        /// <summary>
        /// Gets the Kind of Entity; Person, Place, Thing, Organization, Activity, etc.; that the Entity represents.
        /// </summary>
        EntityKind EntityKind { get; }
    }
}
