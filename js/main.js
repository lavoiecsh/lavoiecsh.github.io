window.onload = function() {
    const categorizor = new Categorizer(Categorizer.getCategoryFromQueryParameters());
    categorizor.categorize();

    const paginator = new Paginator(5);
    paginator.paginate();
};