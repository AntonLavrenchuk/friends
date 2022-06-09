import {Event} from "src/app/Event/Event";

export const EVENTS : Array<Event> = [
    {id: 1, name: "first", organizatorId : 1, membersId : [1,2], coordinates : "10,1 20,3", startDate : new Date(2022, 1)},
    {id: 2, name: "second", organizatorId : 2, membersId : [2,3], coordinates : "10,2 10,3", startDate : new Date(2021, 2)},  
];