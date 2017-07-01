import {LexicalMenuCore} from 'app/models';

export default VerbalMenu;

export interface VerbalMenu extends LexicalMenuCore {
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
export namespace VerbalMenu {}