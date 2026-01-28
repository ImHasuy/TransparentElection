import {createContext} from "react";
import {
    IDCardNumberKeyName,
    IsNationalMinoritiesKeyName,
    NationalMinoritiesEnumName,
    ResidenceCardNumberKeyName, TokenKeyNameForVoter, VotingDistrictName

} from "../constants/constants.ts";
import type AuthContextType from "../interfaces/AuthContextForVoters.ts";

export const AuthContextForVoters = createContext<AuthContextType>({
    tokenForVoter: localStorage.getItem(TokenKeyNameForVoter),
    setTokenForVoter: () => {},
    IDCardNumber: localStorage.getItem(IDCardNumberKeyName),
    setIDCardNumber: () => {},
    ResidenceCardNumber: localStorage.getItem(ResidenceCardNumberKeyName),
    setResidenceCardNumber: () => {},
    IsNationalMinorities: localStorage.getItem(IsNationalMinoritiesKeyName),
    setIsNationalMinorities: () => {},
    NationalMinoritiesEnum: localStorage.getItem(NationalMinoritiesEnumName),
    setNationalMinoritiesEnum: () => {},
    VotingDistrict: localStorage.getItem(VotingDistrictName),
    setVotingDistrict: () => {},
})