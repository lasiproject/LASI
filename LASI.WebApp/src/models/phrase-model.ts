import LexicalModelCore from './lexical-model-core';
import WordModel from './word-model';

export default PhraseModel;

interface PhraseModel extends LexicalModelCore {
  kind: 'phrase';
  words: WordModel[];
}
namespace PhraseModel {}
