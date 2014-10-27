using System;

namespace LASI.Core.Analysis.Melding
{
    public class Lifted<TLexical> : ILexical where TLexical : ILexical
    {
        public double MetaWeight {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public IPrepositional PrepositionOnLeft {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public IPrepositional PrepositionOnRight {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public string Text {
            get {
                throw new NotImplementedException();
            }
        }

        public double Weight {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }
    }
}