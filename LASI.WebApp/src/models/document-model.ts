import TextFragmentModel from './text-fragment-model';

const DocumentModel = {};
export default DocumentModel;

interface DocumentModel extends TextFragmentModel {
  kind: 'document';
  title: string;
  id: string;
  progress: number | string;
  percentComplete: number | string;
}
