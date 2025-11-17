export default interface AuthContext{
    tokenForVoter: string | null;
    setTokenForVoter: (tokenForVoter: string | null) => void;
    IDCardNumber: string | null;
    setIDCardNumber: (IDCardNumber: string | null) => void;
    ResidenceCardNumber: string | null;
    setResidenceCardNumber: (ResidenceCardNumber: string | null) =>void;
    IsNationalMinorities: string | null;
    setIsNationalMinorities: (IsNationalMinorities: string | null) =>void;
    NationalMinoritiesEnum: string | null;
    setNationalMinoritiesEnum: (NationalMinoritiesEnum: string  | null) => void;
    VotingDistrict: string | null;
    setVotingDistrict: (VotingDistrict: string | null) => void;
}