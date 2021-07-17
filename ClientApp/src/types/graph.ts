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
    level: ConceptLevel;
    dependenciesIds: number[];
    graphId: number;
};

export enum ConceptLevel {
    Basic = "Basic",
    Common = "Common",
    Advanced = "Advanced",
    Deep = "Deep",
}
