using LASI.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using LASI.FileSystem.FileTypes;
using System.Threading.Tasks;
using LASI.Algorithm;

namespace LASI.UserInterface.GuiILexicalExtensions
{
    public static class IVerbialExtensions
    {
        /// <summary>
        /// Transforms the Verbal Construct in Subjects GUI Label which includes role specialized context menus, tooltips, and Subjects direct Link to the Verbal as Subjects Lexical.
        /// </summary>
        /// <param name="verbial">The ITransitiveVerbal construct to transforms</param>
        /// <returns>A Specialized Label representing the given Verbal.</returns>
        public static LexicalLabel ToLabel(this ITransitiveVerbal verbial) {
            return new LexicalLabel {
                VisualizedLexical = verbial,
                Content = verbial.Text,
                ContextMenu = CreateContextMenu(verbial)
            };
        }
        /// <summary>
        /// Constructs the context menus for Subjects Verbal Lexical Label.
        /// </summary>
        /// <param name="verbial">The verbial which the lexical Label does or will wrap.</param>
        /// <returns>A new context menu containing functionality specific the verbial instance, its associations, and their own LexicalLabels.</returns>
        private static ContextMenu CreateContextMenu(ITransitiveVerbal verbial) {
            var result = new ContextMenu();
            if (verbial.BoundSubjects.Count() > 0) {
                var referencedSubjectCMI = new MenuItem {
                    Header = "View Subjects"
                };
                referencedSubjectCMI.Click += (s, e) => {
                    var subjectLabels = from sub in verbial.BoundSubjects
                                        join lbl in LexicalLabel.AllLexicalLabels on sub equals lbl.VisualizedLexical
                                        select lbl;
                    foreach (var sb in subjectLabels) {
                        sb.BorderThickness = new Thickness(1);
                        sb.BorderBrush = Brushes.DarkRed;
                    }
                };
                result.Items.Add(referencedSubjectCMI);
            }
            if (verbial.DirectObjects.Count() > 0) {
                var referencedDirectObjectsCMI = new MenuItem {
                    Header = "View Direct Objects"
                };
                referencedDirectObjectsCMI.Click += (s, e) => {
                    var dirObjLabels = from dobj in verbial.DirectObjects
                                       join lbl in LexicalLabel.AllLexicalLabels on dobj equals lbl.VisualizedLexical
                                       select lbl;
                    foreach (var sb in dirObjLabels) {
                        sb.BorderThickness = new Thickness(1);
                        sb.BorderBrush = Brushes.DarkBlue;
                    }
                };
                result.Items.Add(referencedDirectObjectsCMI);
            }
            if (verbial.IndirectObjects.Count() > 0) {
                var referencedIndirectObjectsCMI = new MenuItem {
                    Header = "View Indirect Objects"
                };
                referencedIndirectObjectsCMI.Click += (s, e) => {
                    var indirObjLabels = from idobj in verbial.IndirectObjects
                                         join lbl in LexicalLabel.AllLexicalLabels on idobj equals lbl.VisualizedLexical
                                         select lbl;
                    foreach (var sb in indirObjLabels) {
                        sb.BorderThickness = new Thickness(1);
                        sb.BorderBrush = Brushes.Azure;
                    }
                };
                result.Items.Add(referencedIndirectObjectsCMI);
            }
            return result;
        }
    }
    public class LexicalLabel : Label
    {


        private ILexical visualizedLexical;

        public ILexical VisualizedLexical {
            get {
                return visualizedLexical;
            }
            internal set {
                visualizedLexical = value;
            }
        }

        public LexicalLabel() {
            allLexicalLabels.Add(this);
        }
        private static HashSet<LexicalLabel> allLexicalLabels = new HashSet<LexicalLabel>();

        public static HashSet<LexicalLabel> AllLexicalLabels {
            get {
                return LexicalLabel.allLexicalLabels;
            }
            private set {
                LexicalLabel.allLexicalLabels = value;
            }
        }
    }
}
