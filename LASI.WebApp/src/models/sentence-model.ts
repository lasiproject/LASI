import PhraseModel from './phrase-model';

export default SentenceModel;

interface SentenceModel {
  kind: 'sentence';
  phrases: PhraseModel[];
}
namespace SentenceModel {}