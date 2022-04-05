class GameFigure {
    public view: svgjs.Element;
    public figure: ServerLayer.GameFigures = ServerLayer.GameFigures.None;


    constructor(view: svgjs.Element, figure: ServerLayer.GameFigures) {
        this.view = view.group();
        this.figure = figure;
    }

    show(): GameFigure {
        this.view.show().front();
        return this;
    }

    hide(): GameFigure {
        this.view.hide();
        return this;
    }
}