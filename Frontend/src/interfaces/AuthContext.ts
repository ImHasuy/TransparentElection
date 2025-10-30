export default interface AuthContext{
    token: string | null;
    setToken: (token: string | null) => void;
    nev: string | null;
    setNev: (nev: string | null) =>void;
    email: string | null;
    setEmail: (email: string | null) =>void;
    role: string | null;
    setRole: (role: string) => void;
}