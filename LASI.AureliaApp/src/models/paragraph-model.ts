import {SentenceModel} from 'app/models';

export default ParagraphModel;

interface ParagraphModel {
  kind: 'paragraph';
  sentences: SentenceModel[];
}
namespace ParagraphModel {}