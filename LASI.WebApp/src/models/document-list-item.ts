import {Task} from 'app/models';
import DocumentModel from './document-model';

export default DocumentListItem;

interface DocumentListItem {
  id: string;
  name: string;
  progress: number;
  percentComplete: number;
  showProgress: boolean;
  statusMessage: string;
  raeification: DocumentModel;
  task: Task;
  /**
  * The content is optional as the list item may just be a placeholder for the document.
  */
  content?: string;
}
namespace DocumentListItem {}