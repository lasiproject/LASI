import LexicalModelCore from './lexical-model-core';
import PhraseModel from './phrase-model';

export default ClauseModel;

interface ClauseModel extends LexicalModelCore {
  kind: 'clause';
  phrases: PhraseModel[];
}
namespace ClauseModel {}