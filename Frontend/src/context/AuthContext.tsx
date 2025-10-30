import {createContext} from "react";
import {EmailKeyName, NameKeyName, RoleKeyName, TokenKeyName} from "../constants/constants.ts";
import type AuthContextType from "../interfaces/AuthContext.ts";

export const AuthContext = createContext<AuthContextType>({
    token: localStorage.getItem(TokenKeyName),
    setToken: () => {},
    nev: localStorage.getItem(NameKeyName),
    setNev: () => {},
    email: localStorage.getItem(EmailKeyName),
    setEmail: () => {},
    role: localStorage.getItem(RoleKeyName),
    setRole: () => {},

})