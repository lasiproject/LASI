import LexicalModelCore from 'models/lexical-model-core';

export default WordModel;

interface WordModel extends LexicalModelCore {
  kind: 'word';
}
namespace WordModel {}