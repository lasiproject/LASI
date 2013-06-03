using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm.Binding
{
    public class SubjectVerbObjectResult : ILexical
    {
        public SubjectVerbObjectResult(VerbPhrase verbPhrase) {
            Verbial = verbPhrase;
        }

        public NounPhrase Subject {
            get;
            set;
        }
        public VerbPhrase Verbial {
            get;
            set;
        }
        public NounPhrase DirectObject {
            get;
            set;
        }



        public string Text {
            get {
                return string.Format("{0}\n{1}\n{2}\n", Subject.Text, Verbial.Text, DirectObject.Text);
            }
        }

        public Type Type {
            get {
                return base.GetType();
            }
        }

        public decimal Weight {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public decimal MetaWeight {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
    }
}
