import { jwtDecode } from "jwt-decode";
import {useContext} from "react";
import {AuthContext} from "../context/AuthContext.tsx";
import {
    EmailKeyName,
    EmailTokenKey,
    NameKeyName,
    NameTokenKey, RoleKeyName,
    RoleTokenKey,
    TokenKeyName
} from "../constants/constants.ts";
import api from "../api/api.ts";


const useAuth = () => {
    const {token, setToken, nev, setNev, role, setRole, email, setEmail} = useContext(AuthContext)

    const isLoggedIn = !!token;

    const login = async (email: string, password: string) => {
        // eslint-disable-next-line no-useless-catch
        try{
            const response = await api.Auth.login({email: email, password: password});

            if (response.data.success && response.data.data) {
                const token = response.data.data.token;


                const decoded: any = jwtDecode(token);
                const nev = decoded[NameTokenKey];
                const email = decoded[EmailTokenKey];
                const szerep = decoded[RoleTokenKey];

                setToken(token);
                localStorage.setItem(TokenKeyName, token);

                setNev(nev);
                localStorage.setItem(NameKeyName, nev);

                setEmail(email);
                localStorage.setItem(EmailKeyName, email);

                setRole(szerep);
                localStorage.setItem(RoleKeyName, szerep);

            }else {
                throw new Error(response.data.message || "Ismeretlen hiba");
            }
        }catch (error) {
            throw error;
        }
    }
    const logout=() => {
        localStorage.clear();
        setToken(null);
    }

    return{login, logout, isLoggedIn, token, nev, role, email}
}

export default useAuth;