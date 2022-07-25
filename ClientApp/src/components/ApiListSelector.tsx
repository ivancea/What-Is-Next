import React, { useEffect, useState } from "react";

type Props<T> = {
    url: string;
    elementName: string;
    keySelector: (element: T) => string | number;
    render: (element: T) => React.ReactNode;
    selectedElement?: T;
    onElementSelected: (element?: T) => void;
};

export default function ApiListSelector<T>({
    url,
    elementName,
    keySelector,
    render,
    selectedElement,
    onElementSelected,
}: Props<T>): React.ReactElement {
    const [elements, setElements] = useState<T[]>();

    useEffect(() => {
        fetch(url)
            .then((response) => response.json())
            .then(setElements);
    }, [url]);

    if (selectedElement) {
        return (
            <div>
                <button onClick={() => onElementSelected(undefined)}>Back</button>
                <b>Selected {elementName}:</b> {render(selectedElement)}
            </div>
        );
    }

    return elements ? (
        <ul>
            {elements.map((element) => (
                <button
                    key={keySelector(element)}
                    style={{ cursor: "pointer" }}
                    onClick={() => onElementSelected(element)}
                >
                    {render(element)}
                </button>
            ))}
        </ul>
    ) : (
        <></>
    );
}
