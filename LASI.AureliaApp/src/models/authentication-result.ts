import User from './user';

export default AuthenticationResult;

interface AuthenticationResult {
  user?: User;
  autenticated?: boolean;
  token?: string;
}
namespace AuthenticationResult { }