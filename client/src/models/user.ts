export interface User {
  userName: string;
  email: string;
  token: string;
  roles: Role[];
}

export interface Role {
  name: string;
}
