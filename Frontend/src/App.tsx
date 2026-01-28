import { useState } from 'react'
import { BrowserRouter } from "react-router-dom";
import {
    EmailKeyName, IDCardNumberKeyName,
    IsNationalMinoritiesKeyName,
    NameKeyName, NationalMinoritiesEnumName, ResidenceCardNumberKeyName,
    RoleKeyName,
    TokenKeyName,
    TokenKeyNameForVoter, VotingDistrictName
} from "./constants/constants.ts";
import Routing from "./routing/Routing.tsx";
import { AuthContext } from "./context/AuthContext.tsx";
import {AuthContextForVoters} from "@/context/AuthContextForVoters.tsx";

function App() {

    const [token, setToken] = useState(localStorage.getItem(TokenKeyName));
    const [nev, setNev] = useState(localStorage.getItem(NameKeyName));
    const [role, setRole] = useState(localStorage.getItem(RoleKeyName));
    const [email, setEmail] = useState(localStorage.getItem(EmailKeyName));

    const [tokenForVoter, setTokenForVoter] = useState(localStorage.getItem(TokenKeyNameForVoter));
    const [IDCardNumber, setIDCardNumber] = useState(localStorage.getItem(IDCardNumberKeyName));
    const [ResidenceCardNumber, setResidenceCardNumber] = useState(localStorage.getItem(ResidenceCardNumberKeyName));
    const [IsNationalMinorities, setIsNationalMinorities] = useState(localStorage.getItem(IsNationalMinoritiesKeyName));
    const [NationalMinoritiesEnum, setNationalMinoritiesEnum] = useState(localStorage.getItem(NationalMinoritiesEnumName));
    const [VotingDistrict ,setVotingDistrict] = useState(localStorage.getItem(VotingDistrictName));

    return<BrowserRouter>
            <AuthContext.Provider value={{token, setToken,nev, setNev, role, setRole, email, setEmail}}>
                <AuthContextForVoters.Provider value={{tokenForVoter, setTokenForVoter ,IDCardNumber, setIDCardNumber, ResidenceCardNumber, setResidenceCardNumber, IsNationalMinorities, setIsNationalMinorities, NationalMinoritiesEnum, setNationalMinoritiesEnum,VotingDistrict ,setVotingDistrict}}>
                    <Routing/>
                </AuthContextForVoters.Provider>
            </AuthContext.Provider>
        </BrowserRouter>;


}
export default App