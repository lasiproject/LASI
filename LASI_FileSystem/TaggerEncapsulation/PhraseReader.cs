//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LASI.FileSystem
//{
//    public class PhraseReader
//    {
//        /// <summary>
//        /// Constructs a new instance for extacting phrases from text based on the provided delimiters.
//        /// </summary>
//        /// <param name="delimA">Character which signals the start of a phrase</param>
//        /// <param name="delimB">Character which signals the end of a phrase</param>
//        public PhraseReader(char delimA, char delimB) {
//            Delims = Tuple.Create(delimA, delimB);
//        }
//        /// <summary>
//        /// Extracts a phrase from the given data string and consumes it.
//        /// </summary>
//        /// <param name="data">Provides the textual data from which to read the next phrase</param>
//        /// <returns>The phrase text and its phrase tag or null if no properly delimited phrase was found</returns>
//        public virtual TaggedText? ReadToken(ref string data) {
//            if (String.IsNullOrEmpty(data) || String.IsNullOrWhiteSpace(data))
//                return null;
//            var dl1 = data.IndexOf(Delims.Item1);
//            //if (dl1 < 0)
//            //    return null;
//            var dl2 = data.IndexOf(" " + Delims.Item2, dl1 + 1) + 1;
//            //if (dl2 < 0)
//            //    return null;

//            //throw new UndelimitedPhraseException(String.Format("The given text section, \"{0}\", does not contain a complete phrase of the form \"[phrase]\"",
//            //    data));


//            //Get any text not in tagger identified phrase delimiters "this is not a phrase [this is a phrase]"

//            string ragged = data.Substring(0, dl1 - 1);
//            var len = ragged.Length + dl2 - dl1;
//            var text = data.Substring(0, len - 1);
//            var pTagEnd = text.IndexOfAny(new[] { ' ', '\r', '\t', '\n' }, dl1 + 1);
//            pTagEnd = pTagEnd > -1 ? pTagEnd : text.Length;
//            var resultTag = "";

//            resultTag = text.Substring(dl1 + 1, pTagEnd - dl1 - 1);

//            var resultText = data.Substring(pTagEnd, len - pTagEnd);
//            if (data.Substring(len + 2).IndexOf('/') != -1)
//            {
//                data = data.Substring(len + 2);
//            }
//            else
//                data = data.ToString();
//            return new TaggedText {
//                FrontExcess = ragged,
//                Text = resultText,
//                Tag = resultTag
//            };
//        }
//        protected Tuple<char, char> Delims {
//            get;
//            set;
//        }

//    }
//}
