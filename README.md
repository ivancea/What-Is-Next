# WIN

**What Is Next (*W.I.N.*)** is a collaborative service where users create learning trees/graphs, with levels and dependencies, to learn programming languages, libraries...

Users will be able to submit their own learning graphs and to suggest changes in the graphs of other users.

There will also be a rating system to rank graphs by category/language/etc.

Each graph consists in a set of skills or concepts about the language/library (nodes) that may depend on other concepts that should be *mastered* first. A user will be able to mark his progress. Also, if multiple graphs for the same language/library exist, the progress marked in a concept will be also marked in the same concept in the other graphs, to allow the users to track their progress across different suggested "learning paths".

## TODO

### Model

- Graph: userId - topic (language, library...) - name - description
- Column: graphId - name - description
- Row: graphId - name - description
- Concept: graphId - columnId - rowId - name - description - dependencies

Suggestions for topics columns and rows, ordered by usage (Maybe config entity with default columns, rows...)

## Views

### DEFAULT COLUMNS

- Basic
- Medium
- Advanced
- Tricks

### DEFAULT ROWS

#### Languages

- Basics
- Standard library
- Common libraries -> Navigation to Library graph?

#### Libraries

- Basics
- Utilities