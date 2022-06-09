export interface Event {
    id:number,
    name: string,
    organizatorId: number,
    membersId: Array<number>,
    startDate: Date,
    coordinates: string
};