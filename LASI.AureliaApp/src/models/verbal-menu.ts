import {LexicalMenuCore} from 'models/lexical-menu';

export default VerbalMenu;

interface VerbalMenu extends LexicalMenuCore {
  kind: 'verbal';
  /**
    * The ids of any subjects.
    */
  subjectIds: number[];
  /**
    * The ids of any direct objects.
    */
  directObjectIds: number[];
  /**
    * The ids of any direct objects.
    */
  indirectObjectIds: number[];
}
namespace VerbalMenu {}