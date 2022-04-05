var Cell = /** @class */ (function () {
    function Cell(view, point) {
        this.view = view.group();
        this.point = point;
    }
    Cell.prototype.setFigure = function (figure) {
        this.figure = figure;
        return this;
    };
    Cell.prototype.show = function () {
        this.view.show().front();
        return this;
    };
    Cell.prototype.hide = function () {
        this.view.hide();
        return this;
    };
    return Cell;
}());
//# sourceMappingURL=Cell.js.map