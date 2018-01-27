import SentenceModel from 'models/sentence-model';

export default ParagraphModel;

interface ParagraphModel {
  kind: 'paragraph';
  sentences: SentenceModel[];
}
namespace ParagraphModel {}