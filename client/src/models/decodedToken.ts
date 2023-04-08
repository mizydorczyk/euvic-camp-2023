import {Role} from "./user";

export interface DecodedToken {
  aud: string;
  email: string;
  exp: number;
  iat: number;
  iss: string;
  nameid: string;
  nbf: number;
  role: Role[];
}
