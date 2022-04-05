var GameFigure = /** @class */ (function () {
    function GameFigure(view, figure) {
        this.figure = ServerLayer.GameFigures.None;
        this.view = view.group();
        this.figure = figure;
    }
    GameFigure.prototype.show = function () {
        this.view.show().front();
        return this;
    };
    GameFigure.prototype.hide = function () {
        this.view.hide();
        return this;
    };
    return GameFigure;
}());
//# sourceMappingURL=GameFigure.js.map