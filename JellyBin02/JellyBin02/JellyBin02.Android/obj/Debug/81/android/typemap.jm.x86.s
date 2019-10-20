	/* Data SHA1: ea2497ac3f4a86792a813989b56d0e02f8081f7e */
	.file	"typemap.jm.inc"

	/* Mapping header */
	.section	.data.jm_typemap,"aw",@progbits
	.type	jm_typemap_header, @object
	.p2align	2
	.global	jm_typemap_header
jm_typemap_header:
	/* version */
	.long	1
	/* entry-count */
	.long	606
	/* entry-length */
	.long	234
	/* value-offset */
	.long	104
	.size	jm_typemap_header, 16

	/* Mapping data */
	.type	jm_typemap, @object
	.global	jm_typemap
jm_typemap:
	.size	jm_typemap, 141805
	.include	"typemap.jm.inc"
