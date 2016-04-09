export class DocumentController {
    static $inject = ['$q', 'MockDocumentModelService'];

    private documentModel: DocumentModel;

    constructor(private $q: ng.IQService, private documentModelService: DocumentModelService) { }

    processDocument(id: string) {
        if (this.documentModel.id !== id) {
            return this.documentModelService.processDocument(id);
        } else {
            return this.$q.resolve(this.documentModel);
        }
    }
}