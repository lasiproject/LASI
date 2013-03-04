using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace LASI.Algorithm
{

    public class ToLinker : Word, IPrepositional
    {
        #region Constructors

        public ToLinker()
            : base("to") {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the right-hand-side of the Preposition.
        /// </summary>
        public IPrepositionLinkable OnRightSide {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the left-hand-side of the Preposition.
        /// </summary>
        public IPrepositionLinkable OnLeftSide {
            get;
            set;
        }

        #endregion

        public override XElement Serialize() {
            throw new NotImplementedException();
        }
    }
}
