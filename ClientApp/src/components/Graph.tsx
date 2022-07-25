import go from "gojs";
import React, { useEffect, useState } from "react";
import { config } from "../Config";
import { Concept, ConceptLevel, Graph as GraphType } from "../types/graph";

type Props = {
    selectedGraph: GraphType;
};

function levelToColor(level: ConceptLevel): string {
    switch (level) {
        case ConceptLevel.Basic:
            return "cyan";
        case ConceptLevel.Common:
            return "green";
        case ConceptLevel.Advanced:
            return "orange";
        case ConceptLevel.Deep:
            return "red";
        default:
            return "white";
    }
}

export default function Graph({ selectedGraph }: Props): React.ReactElement {
    const [concepts, setConcepts] = useState<Concept[]>();

    useEffect(() => {
        fetch(`${config.url}/api/graphs/${selectedGraph.id}/concepts`)
            .then((response) => response.json())
            .then((fetchedConcepts) => {
                setConcepts(fetchedConcepts);
            });

        return () => {
            setConcepts(undefined);
        };
    }, [selectedGraph.id]);

    useEffect(() => {
        if (concepts) {
            const $ = go.GraphObject.make;

            const diagram = $(go.Diagram, "diagram", {
                "undoManager.isEnabled": true,
                layout: $(go.LayeredDigraphLayout),
            });

            diagram.nodeTemplate = $(
                go.Node,
                "Auto",
                $(
                    go.Shape,
                    "RoundedRectangle",
                    {
                        strokeWidth: 0,
                    },
                    new go.Binding("fill", "level", levelToColor),
                ),
                $(go.TextBlock, { margin: 8 }, new go.Binding("text", "name")),
            );

            diagram.model = $(go.GraphLinksModel, {
                nodeKeyProperty: "id",
                nodeDataArray: concepts,
                linkDataArray: concepts.flatMap((concept) =>
                    concept.dependenciesIds.map((dependencyId) => ({
                        from: dependencyId,
                        to: concept.id,
                    })),
                ),
            });

            (diagram.layout as go.LayeredDigraphLayout).direction = 90;

            return () => {
                diagram.div = null;
            };
        }
    }, [concepts]);

    return (
        <div>
            <div
                id="diagram"
                style={{ border: "solid 1px black", background: "white", width: "100%", height: "500px" }}
            ></div>

            {concepts
                ? concepts.map((concept) => (
                      <div key={concept.name}>
                          {concept.name} - {concept.description}
                      </div>
                  ))
                : null}
        </div>
    );
}
