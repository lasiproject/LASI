import Credentials from './credentials';
import DocumentModel from './document-model';

export default User;

interface User extends Credentials {
  loggedIn?: boolean;
  email: string;
  password: string;
  documents: DocumentModel[];
  id: string;
}

namespace User {}