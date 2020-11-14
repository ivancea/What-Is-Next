export type Graph = {
    id: number;
    topic: string;
    name: string;
    description: string;
};

export type Concept = {
    id: number;
    name: string;
    description: string;
    dependenciesIds: number[];
    graphId: number;
};
