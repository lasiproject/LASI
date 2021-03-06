﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Content.Tagging
{
    internal class PhraseExtractor
    {
        public TextNode ExtractAndConsume(ref string phraseString)
        {
            phraseString = phraseString.Trim();
            var tagStart = 0;
            var tagEnd = phraseString.Substring(tagStart).IndexOf(" (", StringComparison.Ordinal);
            if (tagEnd == -1 || tagEnd < tagStart)
            {
                var tt = phraseString.SplitRemoveEmpty(' ');
                var taggedPair = new TaggedText(text: tt[1], tag: tt[0]);
                phraseString = string.Empty;
                return new TextNode(taggedPair.Tag + ":  " + taggedPair.Text);
            }
            var tagLength = tagEnd;
            var tag = phraseString.Substring(tagStart + 1, tagLength - 1);
            var innerTextEnd = phraseString.IndexOf("))", StringComparison.Ordinal);
            var innerTextLen = innerTextEnd - tagEnd + 1;
            var innerText = phraseString.Substring(tagEnd + 1, innerTextLen);
            if (innerText.Count(c => c == '(') > 0)
            {
                phraseString = phraseString.Substring(tagLength + innerText.Length).Trim();
                var taggedPair = new TaggedText(tag: tag, text: innerText);
                var result = new TextNode(taggedPair.Tag + ":  " + taggedPair.Text);
                result.AppentChild(ExtractAndConsume(ref innerText));
                return result;
            }
            else
            {
                phraseString = phraseString.Substring(innerTextEnd + 1).Trim();
                var tagged = new TaggedText(tag: "X", text: string.Format("({0} {1})", tag, innerText));
                var result = new TextNode(tagged.Tag + ":  " + tagged.Text);
                result.AppentChild(ExtractAndConsume(ref innerText));
                return result;
            }
        }
    }
    class TextNode
    {
        public TextNode(string text) => Text = text;

        public override string ToString() => Children.Aggregate(Text, (result, child) => result + child);

        public void AppentChild(TextNode child)
        {
            children.Add(child);
        }

        public string Text { get; }

        public IReadOnlyList<TextNode> Children => children;

        readonly List<TextNode> children = new List<TextNode>();
    }
}
