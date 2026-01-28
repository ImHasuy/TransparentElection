// SPDX-License-Identifier: MIT
pragma solidity ^0.8.24;

contract Voting {
    address public owner;

    struct VoteData {
        string partyVote;
        string singleMemberVote;
        bool hasVoted;
        uint256 timestamp;
    }


    mapping(address => VoteData) public votes;


    mapping(string => uint256) public partyVoteCounts;
    mapping(string => uint256) public memberVoteCounts;


    event VoteCast(address indexed voter, string party, string member, uint256 timestamp);

    constructor() {
        owner = msg.sender;
    }

    function vote(string memory _party, string memory _member, address _voter) external {
        require(msg.sender == owner, "Only backend can submit");
        require(!votes[_voter].hasVoted, "Already voted");

        votes[_voter] = VoteData({
            partyVote: _party,
            singleMemberVote: _member,
            hasVoted: true,
            timestamp: block.timestamp
        });

        partyVoteCounts[_party]++;
        memberVoteCounts[_member]++;

        emit VoteCast(_voter, _party, _member, block.timestamp);
    }


    function getPartyCount(string memory _partyId) external view returns (uint256) {
        return partyVoteCounts[_partyId];
    }


    function getMemberCount(string memory _memberId) external view returns (uint256) {
        return memberVoteCounts[_memberId];
    }

    function getUserVote(address _user) external view returns (string memory party, string memory member) {
        require(votes[_user].hasVoted, "User has not voted");
        return (votes[_user].partyVote, votes[_user].singleMemberVote);
    }
}