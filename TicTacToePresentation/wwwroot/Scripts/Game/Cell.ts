class Cell {
    public view: svgjs.Element;
    public figure: GameFigure;
    public point: Point;

    constructor(view: svgjs.Element, point: Point) {
        this.view = view.group();
        this.point = point;
    }

    setFigure(figure: GameFigure): Cell{
        this.figure = figure;
        return this;
    }

    show(): Cell {
        this.view.show().front();
        return this;
    }

    hide(): Cell {
        this.view.hide();
        return this;
    }
}